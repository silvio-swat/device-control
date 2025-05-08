export interface CommandParameter {
  name: string;
  description: string;
}

export interface DeviceCommand {
  command: string;
  parameters: CommandParameter[];
}

export interface CommandDescription {
  operation: string;
  description: string;
  command: DeviceCommand;
  result: string;
  format: string;
}

export interface Device {
  identifier: string;
  description: string;
  manufacturer: string;
  url: string;
  commands: CommandDescription[];
}
