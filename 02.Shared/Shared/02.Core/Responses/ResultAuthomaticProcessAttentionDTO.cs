using SharedClasses._02.Core.DTOs;

namespace SharedClasses._02.Core.Responses;
public record ResultAuthomaticProcessAttentionDTO(bool Success, string Message, List<ResultProcessAttentionDTO>? LstResultProcessAttentionsDTO = null);