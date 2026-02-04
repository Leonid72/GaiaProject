export interface OperationDto {
  id: number;
  name: string;
  displayName: string;
  description: string;
  operationType: string;
  isActive: boolean;
  sortOrder: number;
}

export interface OperationExecuteRequestDto {
  operationName: string;
  fieldA: string;
  fieldB: string;
}

export interface OperationHistoryItemDto {
  fieldA: string;
  fieldB: string;
  result: string;
  executedAt: string;
}

export interface OperationHistoryInfoDto {
  lastThreeOperations: OperationHistoryItemDto[];
  monthlyOperationCount: number;
}

export interface OperationExecuteResponseDto {
  isSuccess: boolean;
  result: string;
  errorMessage?: string;
  historyInfo?: OperationHistoryInfoDto;
}

// Your backend CreateOperationDto (used for create/update)
export interface CreateOperationDto {
  name: string;
  displayName: string;
  description: string;
  operationType: string;
  implementationClass: string;
  sortOrder: number;
}

// Optional: if your GET by id returns implementationClass + isActive, this matches it.
export interface OperationDetailsDto extends OperationDto {
  implementationClass?: string;
}
