import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CiotdService } from './ciotd.service';
import { environment } from '../../environments/environment';

describe('CiotdService', () => {
  let service: CiotdService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CiotdService]
    });

    service = TestBed.inject(CiotdService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('deve retornar lista de dispositivos', () => {
    const mockDevices = ['device-1', 'device-2'];

    service.getDevices().subscribe(devices => {
      expect(devices).toEqual(mockDevices);
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/ciotd-proxy/devices`);
    expect(req.request.method).toBe('GET');
    req.flush(mockDevices);
  });
});
