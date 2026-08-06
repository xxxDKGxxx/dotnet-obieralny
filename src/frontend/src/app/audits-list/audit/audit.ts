import { Component, Input } from '@angular/core';
import { AuditDto } from '../../services/audit/audit-model';
import { MatCardModule } from '@angular/material/card';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-audit',
  imports: [MatCardModule, JsonPipe],
  templateUrl: './audit.html',
})
export class Audit {
  @Input({ required: true })
  audit!: AuditDto;
}
