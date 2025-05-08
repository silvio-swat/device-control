import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CommandService } from '../../services/command.service';
import { Device } from '../../models/device.model';
import { Router } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';


@Component({
  selector: 'app-command-execution',
  imports: [SharedStandaloneModule, ReactiveFormsModule],
  templateUrl: './command-execution.component.html',
  styleUrl: './command-execution.component.scss'
})
export class CommandExecutionComponent implements OnInit {
  form!: FormGroup;
  command: any;
  device!: Device;
  result: string = "";

  constructor(
    private route: ActivatedRoute,
    private deviceService: ApiService,
    private commandService: CommandService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  async ngOnInit() {
    const deviceId = this.route.snapshot.params['id'];
    const commandId = this.route.snapshot.params['commandId'];

    if (deviceId) {
      this.deviceService.getDeviceById(deviceId).subscribe({
        next: (data) => {
          this.device = data;
          this.command = this.device.commands.find((c: any) => c.operation === commandId);

          this.initForm();
        },
        error: () => this.router.navigate(['/devices'])
      });
    }

    // this.command = this.device.commands.find((c: any) => c.operation === commandId);

    // this.initForm();
  }

  private initForm() {
    const controls: any = {};
    this.command.command.parameters.forEach((param: any) => {
      controls[param.name] = [null, Validators.required];
    });

    this.form = this.fb.group(controls);
  }

  onSubmit() {
    const deviceId = this.route.snapshot.params['id'];
    this.commandService.executeCommand(
      deviceId,
      this.command.operation,
      this.form.value
    ).subscribe({
      next: (res) => {
        this.result = res;
      },
      error: (err) => console.error('Erro na execução:', err)
    });
  }
}
