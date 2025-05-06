import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../models/device.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5112'; // usamos proxy, sem URL completa

  private http = inject(HttpClient);

  getToken(credentials: any): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(
      '/auth/token',
      credentials
    );
  }

  getDevices(): Observable<Device[]> {
    return this.http.get<Device[]>('/api/devices');
  }

  getDeviceById(id: string): Observable<Device> {
    return this.http.get<Device>(`/api/devices/${id}`);
  }

}
