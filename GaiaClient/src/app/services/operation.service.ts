import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable, shareReplay } from 'rxjs';
import { CreateOperationDto, OperationDetailsDto, OperationDto, OperationExecuteRequestDto, OperationExecuteResponseDto } from '../models/operation.model';

@Injectable({
  providedIn: 'root'
})
export class OperationService {

  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  // Calculator dropdown: active only — shared so multiple subscribers do not re-fire the request
  readonly activeOperations$: Observable<OperationDto[]> =
    this.http.get<OperationDto[]>(`${this.baseUrl}/api/Operations/active`).pipe(shareReplay(1));


  // Manage page: all
  getAll(): Observable<OperationDto[]> {
    return this.http.get<OperationDto[]>(`${this.baseUrl}/api/Operations`);
  }

  // Optional details (if needed for update payload)
  getById(id: number): Observable<OperationDetailsDto> {
    return this.http.get<OperationDetailsDto>(`${this.baseUrl}/api/Operations/${id}`);
  }

  execute(req: OperationExecuteRequestDto): Observable<OperationExecuteResponseDto> {
    return this.http.post<OperationExecuteResponseDto>(`${this.baseUrl}/api/Operations/execute`, req);
  }

  update(id: number, dto: CreateOperationDto & { isActive?: boolean }): Observable<OperationDto> {
    return this.http.put<OperationDto>(`${this.baseUrl}/api/Operations/${id}`, dto);
  }

  updateStatus(id: number, isActive: boolean): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/api/Operations/${id}/status`, { isActive });
  }
  
}
