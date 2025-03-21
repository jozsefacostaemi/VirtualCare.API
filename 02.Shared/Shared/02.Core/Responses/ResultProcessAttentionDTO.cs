namespace SharedClasses._02.Core.DTOs;
public record ResultProcessAttentionDTO(bool Success, string Message, AttentionDTO? AttentionDTO = null);