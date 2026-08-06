import { Component, DestroyRef, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { AuditDto } from '../services/audit/audit-model';
import { AuditService } from '../services/audit/audit-service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Audit } from './audit/audit';

@Component({
  selector: 'app-audits-list',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    Audit,
  ],
  templateUrl: './audits-list.html',
  providers: [provideNativeDateAdapter()],
})
export class AuditsList {
  protected selectedDate!: Date;
  protected auditsList: AuditDto[] = [];

  private readonly auditsService = inject(AuditService);
  private readonly destroyRef = inject(DestroyRef);

  protected updateAuditsList() {
    const copy = new Date(this.selectedDate);

    copy.setMinutes(copy.getMinutes() - copy.getTimezoneOffset());

    this.auditsService
      .listAudits(copy)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (audits) => {
          this.auditsList = audits;
        },
      });
  }
}
