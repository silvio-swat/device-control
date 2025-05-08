import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class CommandService {
  private apiUrl = 'https://localhost:7112/api';
  constructor(private http: HttpClient) { }

  executeCommand(deviceId: string, commandName: string, params: any): Observable<any> {
    const payload = {
      deviceId,
      parameters: params
    };

    return this.http.post(
      `/api/cmds/${commandName}`,
      payload,
      {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      }
    );
  }
}
