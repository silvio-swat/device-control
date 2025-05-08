import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandExecutionComponent } from './command-execution.component';

describe('CommandExecutionComponent', () => {
  let component: CommandExecutionComponent;
  let fixture: ComponentFixture<CommandExecutionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandExecutionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandExecutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
