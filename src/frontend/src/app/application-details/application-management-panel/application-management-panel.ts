import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import {
  ApplicationStatus,
  ApplicationWithProviderTypeDto,
} from '../../services/applications/applications-model';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserDto, UserRoles } from '../../services/auth.model';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ApplicationsService } from '../../services/applications/applications-service';
import { FormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialog } from '../../common/confirm-dialog/confirm-dialog';
import { filter, switchMap } from 'rxjs';
import { AppStatusPipe } from '../../common/app-status-pipe';

@Component({
  selector: 'app-application-management-panel',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatDialogModule,
    AppStatusPipe,
  ],
  templateUrl: './application-management-panel.html',
})
export class ApplicationManagementPanel implements OnInit {
  @Input({ required: true })
  application!: ApplicationWithProviderTypeDto;

  protected user!: UserDto;
  protected userRoles = UserRoles;
  protected applicationStatus = ApplicationStatus;
  protected statusStateMachine = ApplicationStatusStateMachine;
  protected statusChangeMessage: string | null = null;

  private readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly applicationsService = inject(ApplicationsService);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialog = inject(MatDialog);
  private readonly appStatusPipe = new AppStatusPipe();

  ngOnInit(): void {
    this.authService
      .getUserProfile()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((user) => {
        this.user = user;
      });

    this.statusChangeMessage = this.application.lastStatusChangeMessage;
  }

  protected doesEmployeeHaveAnyActions() {
    return (
      this.statusStateMachine.canEmployeeApprove(this.application.status) ||
      this.statusStateMachine.canEmployeeReject(this.application.status) ||
      this.statusStateMachine.canEmployeeRequestChanges(this.application.status)
    );
  }

  protected updateStatus(newStatus: ApplicationStatus) {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {
        message: `Czy na pewno chcesz zmienić status na "${this.appStatusPipe.transform(newStatus)}"?`,
      },
      width: '400px',
    });

    dialogRef
      .afterClosed()
      .pipe(
        filter((result) => result === true),
        switchMap(() => {
          return this.performStatusUpdate(newStatus);
        }),
      )
      .subscribe({
        next: (val) => {
          this.application = val;
          this.statusChangeMessage = this.application.lastStatusChangeMessage;
        },
        error: () => {
          this.snackBar.open('Wystąpił błąd podczas zmieniania statusu');
        },
      });
  }

  protected employeeApprove() {
    if (!this.statusStateMachine.canEmployeeApprove(this.application.status)) {
      return;
    }

    this.updateStatus(this.statusStateMachine.employeeApprove(this.application.status));
  }

  protected employeeReject() {
    if (!this.statusStateMachine.canEmployeeReject(this.application.status)) {
      return;
    }

    this.updateStatus(this.statusStateMachine.employeeReject(this.application.status));
  }

  protected employeeRequestChanges() {
    if (!this.statusStateMachine.canEmployeeRequestChanges(this.application.status)) {
      return;
    }

    this.updateStatus(this.statusStateMachine.employeeRequestChanges(this.application.status));
  }

  protected userWithdraw() {
    if (!this.statusStateMachine.canUserWithdraw(this.application.status)) {
      return;
    }

    this.updateStatus(this.statusStateMachine.userWithdraw(this.application.status));
  }

  protected userUploadDocument() {
    if (!this.statusStateMachine.canUserUploadDocument(this.application.status)) {
      return;
    }

    this.updateStatus(ApplicationStatus.Signed);
  }

  private performStatusUpdate(newStatus: ApplicationStatus) {
    return this.applicationsService.updateApplicationStatus(this.application.id, {
      newStatus: newStatus,
      providerType: this.application.providerType,
      statusChangeMessage: this.statusChangeMessage,
    });
  }
}

class ApplicationStatusStateMachine {
  public static employeeApprove(status: ApplicationStatus) {
    if (status === ApplicationStatus.Created) {
      return ApplicationStatus.AwaitingSignature;
    }

    if (status === ApplicationStatus.Signed) {
      return ApplicationStatus.Granted;
    }

    return status;
  }

  public static canEmployeeApprove(status: ApplicationStatus) {
    return status === ApplicationStatus.Created || status == ApplicationStatus.Signed;
  }

  public static employeeReject(status: ApplicationStatus) {
    if (this.canEmployeeReject(status)) {
      return ApplicationStatus.Rejected;
    }

    return status;
  }

  public static canEmployeeReject(status: ApplicationStatus) {
    return status === ApplicationStatus.Signed || status == ApplicationStatus.Created;
  }

  public static employeeRequestChanges(status: ApplicationStatus) {
    if (this.canEmployeeRequestChanges(status)) {
      return ApplicationStatus.AwaitingAmendments;
    }

    return status;
  }

  public static canEmployeeRequestChanges(status: ApplicationStatus) {
    return status === ApplicationStatus.Signed;
  }

  public static canUserWithdraw(status: ApplicationStatus) {
    return (
      status === ApplicationStatus.Created ||
      status === ApplicationStatus.AwaitingSignature ||
      status === ApplicationStatus.Signed ||
      status === ApplicationStatus.AwaitingAmendments
    );
  }

  public static userWithdraw(status: ApplicationStatus) {
    if (this.canUserWithdraw(status)) {
      return ApplicationStatus.Withdrawn;
    }

    return status;
  }

  public static canUserUploadDocument(status: ApplicationStatus) {
    return (
      status === ApplicationStatus.AwaitingAmendments ||
      status === ApplicationStatus.AwaitingSignature
    );
  }
}
