import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';
import { DeviceService } from '../../services/device.service';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [
    SharedStandaloneModule
  ],
  templateUrl: './device-list.component.html',
  styleUrl: './device-list.component.scss'
})
export class DeviceListComponent implements OnInit {
  devices: string[] = [];

  constructor(
    private router: Router,
    private DeviceService: DeviceService
  ) { }

  ngOnInit() {
    this.loadDevices();
  }

  private loadDevices() {
    this.DeviceService.getDevices().subscribe({
      next: (data) => this.devices = data,
      error: () => alert('Erro ao carregar dispositivos')
    });
  }

  viewDetails(deviceId: string) {
    this.router.navigate(['/device-details', deviceId]);
  } 
}
