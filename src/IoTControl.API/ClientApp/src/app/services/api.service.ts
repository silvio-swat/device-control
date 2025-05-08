import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../models/device.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7112/api';

  private http = inject(HttpClient);

  getDevices(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/devices`, {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    });
  }

  getDeviceById(id: string): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/devices/${id}`, {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    });
  }

}
