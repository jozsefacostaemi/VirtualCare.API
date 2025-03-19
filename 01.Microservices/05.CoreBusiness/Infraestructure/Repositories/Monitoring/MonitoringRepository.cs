﻿using System.Diagnostics;
using Domain.Enums;
using Domain.Interfaces.Monitoring;
using Infraestructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infraestructure.Repositories.Monitoring
{
    public class MonitoringRepository : IMonitoringRepository
    {
        private readonly ApplicationDbContext _context;
        public MonitoringRepository(ApplicationDbContext context) => _context = context;

        /* Función que valida las credenciales del personal asistencial */
        public async Task<RequestResult> GetQuantityByState(Guid? BusinessLineId)
        {
            Guid BusinessLine = BusinessLineId.HasValue ? BusinessLineId.Value : Guid.Parse("DD44C571-4FA5-4133-AECD-062834C93601");

            // Obtener el LevelQueueId basado en el BusinessLineId
            Guid levelQueueByBusinessLine = await _context.BusinessLines
                .Where(x => x.Id.Equals(BusinessLine))
                .Select(x => x.LevelQueueId)
                .FirstOrDefaultAsync();

            // Obtener los estados de atención activos
            var getStatesAttention = await _context.MedicalRecordStates
                //.Where(x => x.Active == true)
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();

            if (!getStatesAttention.Any())
                return RequestResult.SuccessResultNoRecords();

            // Obtener las atenciones agrupadas por proceso y estado de atención
            var attentionsByState = await _context.MedicalRecords
                .Where(x => getStatesAttention.Select(s => s.Id).Contains(x.MedicalRecordStateId))
                .GroupBy(x => new { x.ServiceId, ProcessName = x.Service != null ? x.Service.Name : string.Empty, StateAttentionName = x.MedicalRecordState.Name, x.MedicalRecordStateId })
                .Select(g => new
                {
                    ProcessName = g.Key.ProcessName,
                    StateAttentionName = g.Key.StateAttentionName,
                    Count = g.Count(),
                    StateAttentionId = g.Key.MedicalRecordStateId
                })
                .ToListAsync();

            var color = GetColor.GetRandomColorPair();

            var groupedByProcess = attentionsByState
                .GroupBy(x => x.ProcessName)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .OrderBy(x => x.StateAttentionId)
                        .Select(x =>
                        {
                            var color = GetColor.GetRandomColorPair();
                            return new
                            {
                                Value = x.StateAttentionName,
                                Count = x.Count,
                                Background = color.backgroundColor,
                                Border = color.borderColor
                            };
                        })
                        .ToArray()
                );


            var finalResult = groupedByProcess.Select(group => new
            {
                ProcessName = group.Key,
                Data = group.Value
            }).ToList();

            return RequestResult.SuccessResult(finalResult);
        }

        /* Función que obtiene información de la CPU */
        public async Task<RequestResult> GetUsageCPU()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            float cpuUsage = cpuCounter.NextValue();
            return RequestResult.SuccessResult(cpuUsage);
        }
        /* Función que consulta la información de doctores agrupados por atenciones */
        public async Task<RequestResult> GetAttentionsFinishByHealthCareStaff(Guid? BusinessLineId)
        {
            var attentionsFinishByHealthCareStaff = await _context.MedicalRecords
                .Where(x => x.User != null && !string.IsNullOrEmpty(x.MedicalRecordState.Code))
                .GroupBy(x => new { x.UserId, UserName = x.User != null ? x.User.UserName : string.Empty })
                .Select(x => new
                {
                    HealthCareStaffName = x.Key.UserName,
                    Count = x.Count()
                }).ToListAsync();
            return RequestResult.SuccessResult(attentionsFinishByHealthCareStaff?.Select(x =>
            {
                var colorPair = GetColor.GetRandomColorPair();
                return new
                {
                    Value = x.HealthCareStaffName,
                    Count = x.Count,
                    Background = colorPair.backgroundColor,
                    Border = colorPair.borderColor
                };
            }));
        }
        /* Función que consulta la información de doctores logueados por atenciones OK */
        public async Task<RequestResult> GetLogguedHealthCareStaff(Guid? BusinessLineId)
        {
            var groupedUsers = await _context.Users
                .GroupBy(u => u.Loggued)
                .Select(g => new
                {
                    Logged = g.Key,
                    Users = g.ToList()
                })
                .ToListAsync();
            var loggedUsers = groupedUsers.FirstOrDefault(g => g.Logged == true)?.Users;
            var notLoggedUsers = groupedUsers.FirstOrDefault(g => g.Logged == false)?.Users;

            List<dynamic> lstUsers = new List<dynamic>();

            var colorPairLoggued = GetColor.GetRandomColorPair();
            lstUsers.Add(new
            {
                Value = "Online",
                Count = loggedUsers?.Count,
                Background = colorPairLoggued.backgroundColor,
                Border = colorPairLoggued.borderColor
            });

            var colorPairNoLoggued = GetColor.GetRandomColorPair();
            lstUsers.Add(new
            {
                Value = "Offline",
                Count = notLoggedUsers?.Count,
                Background = colorPairNoLoggued.backgroundColor,
                Border = colorPairNoLoggued.borderColor
            });
            return RequestResult.SuccessResult(lstUsers);
        }

        /* Función que consulta los datos de de números de atenciones en el tiempo */
        public async Task<RequestResult> GetAttentionsByTimeLine(Guid? BusinessLineId)
        {
            var result = await _context.MedicalRecords
                .Where(a => a.CreatedAt >= DateTime.Now.AddDays(-10))
                .GroupBy(a => new { Year = a.CreatedAt.Value.Year, Month = a.CreatedAt.Value.Month, Day = a.CreatedAt.Value.Day })
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date.Year)
                .ThenBy(x => x.Date.Month)
                .ThenBy(x => x.Date.Day)
                .ToListAsync();
            return RequestResult.SuccessResult(result?.Select(x =>
            {
                var colorPair = GetColor.GetRandomColorPair();
                return new
                {
                    Value = $"{x.Date.Year}-{x.Date.Month:00}-{x.Date.Day:00}",
                    Count = x.Count,
                    Background = colorPair.backgroundColor,
                    Border = colorPair.borderColor
                };
            }));
        }

        /* Función que consulta los datos de las atenciones finalizadas */
        public async Task<RequestResult> GetPercentAttentionsFinish(Guid? BusinessLineId)
        {
            var resultado = await _context.MedicalRecords
                .GroupBy(a => a.MedicalRecordState.Code)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            var totalAtenciones = resultado.Sum(r => r.Count);
            var atencionesPendiente = resultado.FirstOrDefault(r => r.Status.Equals(MedicalRecordStateEnum.PENDING.ToString()))?.Count ?? 0;
            var atencionesAsignadas = resultado.FirstOrDefault(r => r.Status.Equals(MedicalRecordStateEnum.ASSIGNED.ToString()))?.Count ?? 0;
            var atencionesEnProceso = resultado.FirstOrDefault(r => r.Status.Equals(MedicalRecordStateEnum.INPROCESS.ToString()))?.Count ?? 0;
            var atencionesFinalizadas = resultado.FirstOrDefault(r => r.Status.Equals(MedicalRecordStateEnum.ATTENDED.ToString()))?.Count ?? 0;
            var atencionesCanceladas = resultado.FirstOrDefault(r => r.Status.Equals(MedicalRecordStateEnum.CANCELLED.ToString()))?.Count ?? 0;

            double porcentajeAtencionesPendiente = 0;
            double porcentajeAtencionesAsignada = 0;
            double porcentajeAtencionesEnproceso = 0;
            double porcentajeAtencionesFinalizadas = 0;
            double porcentajeAtencionesCanceladas = 0;
            if (totalAtenciones > 0)
            {
                porcentajeAtencionesPendiente = (double)atencionesPendiente / totalAtenciones * 100;
                porcentajeAtencionesAsignada = (double)atencionesAsignadas / totalAtenciones * 100;
                porcentajeAtencionesEnproceso = (double)atencionesEnProceso / totalAtenciones * 100;
                porcentajeAtencionesFinalizadas = (double)atencionesFinalizadas / totalAtenciones * 100;
                porcentajeAtencionesCanceladas = (double)atencionesCanceladas / totalAtenciones * 100;

            }
            List<dynamic> lstAttentionTypes = new List<dynamic>();
            var colorPairOne = GetColor.GetRandomColorPair();
            var colorPairTwo = GetColor.GetRandomColorPair();
            var colorPairThree = GetColor.GetRandomColorPair();
            var colorPairFour = GetColor.GetRandomColorPair();
            var colorPairFive = GetColor.GetRandomColorPair();
            lstAttentionTypes.Add(new
            {
                Value = "Pendientes",
                Count = porcentajeAtencionesPendiente,
                Background = colorPairOne.backgroundColor,
                Border = colorPairOne.borderColor
            });

            lstAttentionTypes.Add(new
            {
                Value = "Asignadas",
                Count = porcentajeAtencionesAsignada,
                Background = colorPairTwo.backgroundColor,
                Border = colorPairTwo.borderColor
            });


            lstAttentionTypes.Add(new
            {
                Value = "En proceso",
                Count = porcentajeAtencionesEnproceso,
                Background = colorPairThree.backgroundColor,
                Border = colorPairThree.borderColor
            });

            lstAttentionTypes.Add(new
            {
                Value = "Finalizadas",
                Count = porcentajeAtencionesFinalizadas,
                Background = colorPairFour.backgroundColor,
                Border = colorPairFour.borderColor
            });

            lstAttentionTypes.Add(new
            {
                Value = "Canceladas",
                Count = porcentajeAtencionesCanceladas,
                Background = colorPairFive.backgroundColor,
                Border = colorPairFive.borderColor
            });
            return RequestResult.SuccessResult(lstAttentionTypes);

        }

        /* Función que consulta los datos de ciudades por paciente : OK */
        public async Task<RequestResult> GetNumberAttentionsByCity(Guid? BusinessLineId)
        {
            var resultByCity = await _context.MedicalRecords
                .GroupBy(a => a.Patient.City.Name)
                .Select(g => new
                {
                    City = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            return RequestResult.SuccessResult(resultByCity?.Select(x =>
            {
                var colorPair = GetColor.GetRandomColorPair();
                return new
                {
                    Value = x.City,
                    x.Count,
                    Background = colorPair.backgroundColor,
                    Border = colorPair.borderColor
                };
            }));
        }

        /* Función que consulta las colas activas */
        public async Task<RequestResult> GetQueuesActive(Guid? businessLineId)
        {
            businessLineId = businessLineId == null || businessLineId == Guid.Empty ? Guid.Parse("DD44C571-4FA5-4133-AECD-062834C93601") : businessLineId;

            Guid getLevelQueueId = await _context.BusinessLines.Where(x => x.Id.Equals(businessLineId)).Select(x => x.LevelQueueId).FirstOrDefaultAsync();

            var confQueues = await _context.GeneratedQueues.Where(x => x.QueueConfId != null && x.QueueConf.BusinessLineLevelValueQueueConf != null && x.QueueConf.BusinessLineLevelValueQueueConf.LevelQueueId.Equals(getLevelQueueId)).Select(x => new { Order = x.QueueConf.NOrder, Name = x.Name }).OrderByDescending(x => x.Order).ToListAsync();

            if (confQueues != null)
            {
                return RequestResult.SuccessResult(confQueues?.Select(x =>
                {
                    var colorPair = GetColor.GetRandomColorPair();
                    return new
                    {
                        Value = x.Name,
                        Count = 1,
                        Background = colorPair.backgroundColor,
                        Border = colorPair.borderColor
                    };
                }));

            }
            return RequestResult.SuccessResultNoRecords();
        }

        /* Función que indica el numero de colas activas */
        public async Task<RequestResult> GetNumberActive(Guid? businessLineId)
        {
            businessLineId = businessLineId == null || businessLineId == Guid.Empty ? Guid.Parse("DD44C571-4FA5-4133-AECD-062834C93601") : businessLineId;

            Guid getLevelQueueId = await _context.BusinessLines.Where(x => x.Id.Equals(businessLineId)).Select(x => x.LevelQueueId).FirstOrDefaultAsync();

            var confQueues = await _context.GeneratedQueues.Where(x => x.QueueConf != null && x.QueueConf.BusinessLineLevelValueQueueConf != null && x.QueueConf.BusinessLineLevelValueQueueConf.LevelQueueId.Equals(getLevelQueueId)).Select(x => new { Order = x.QueueConf.NOrder, Name = x.Name }).OrderByDescending(x => x.Order).ToListAsync();

            List<dynamic> lstUsers = new List<dynamic>();
            var colorPair = GetColor.GetRandomColorPair();

            if (confQueues?.Count > 0)
            {
                lstUsers.Add(new
                {
                    Value = confQueues.Count(),
                    Count = 1,
                    Background = colorPair.backgroundColor,
                    Border = colorPair.borderColor
                });
                return RequestResult.SuccessResult(lstUsers);
            }
            return RequestResult.SuccessResultNoRecords();
        }
    }
}
