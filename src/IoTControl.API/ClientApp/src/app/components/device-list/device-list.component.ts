import { Component, OnInit } from '@angular/core';
import { CiotdService } from '../../services/ciotd.service';
import { Router, RouterModule } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';


@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [
    SharedStandaloneModule
  ],
  templateUrl: './device-list.component.html',
  styleUrl: './device-list.component.scss'
})
export class DeviceListComponent {
  devices: string[] = [];

  constructor(
    private ciotdService: CiotdService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadDevices();
  }

  private loadDevices() {
    this.ciotdService.getDevices().subscribe({
      next: (devices) => this.devices = devices,
      error: (err) => console.error('Erro ao carregar dispositivos', err)
    });
  }

  viewDetails(deviceId: string) {
    this.router.navigate(['/devices', deviceId]);
  } 
}
