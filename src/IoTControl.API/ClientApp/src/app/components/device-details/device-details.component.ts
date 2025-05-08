import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model'; 

@Component({
  selector: 'app-device-details',
  imports: [SharedStandaloneModule],
  templateUrl: './device-details.component.html',
  styleUrl: './device-details.component.scss'
})
export class DeviceDetailsComponent implements OnInit {
  device!: Device;
  constructor(
    private route: ActivatedRoute,
    private DeviceService: DeviceService,
    private router: Router
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.DeviceService.getDeviceById(id).subscribe({
        next: (data) => this.device = data,
        error: () => this.router.navigate(['/devices'])
      });
    }
  }

  executeCommand(cmd: any) {
    // Adicione a lógica de navegação ou execução de comando
    const deviceId = this.route.snapshot.paramMap.get('id');
    this.router.navigate([
      '/device-details',
      deviceId,
      'execute-command',
      cmd.operation
    ]);
  }

}
