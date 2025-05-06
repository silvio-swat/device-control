import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CiotdService {
  private baseUrl = `${environment.apiUrl}/ciotd-proxy`;

  constructor(private http: HttpClient) { }

  private getAuthHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
  }

  getDevices(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/devices`, {
      headers: this.getAuthHeaders()
    });
  }

  getDeviceDetails(id: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/devices/${id}`, {
      headers: this.getAuthHeaders()
    });
  }
}
