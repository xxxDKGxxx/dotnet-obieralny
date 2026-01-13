import { Component, inject, OnInit, ChangeDetectorRef, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { UserDto } from '../services/auth.model';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
  ],
  templateUrl: './user-profile.html',
})
export class UserProfileComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly userService = inject(UserService);
  private readonly snackBar = inject(MatSnackBar);
  private readonly cdr = inject(ChangeDetectorRef);
  private readonly destroyRef = inject(DestroyRef);

  protected userForm!: FormGroup;
  protected currentUser: UserDto | null = null;

  constructor() {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      firstName: ['', [Validators.maxLength(50)]],
      lastName: ['', [Validators.maxLength(50)]],
      address: ['', [Validators.maxLength(200)]],
      phone: ['', [Validators.pattern(/^[0-9]{9}$/)]],
      job: ['', [Validators.maxLength(100)]],
      income: [null, [Validators.min(0)]],
      costs: [null, [Validators.min(0)]],
      age: [null, [Validators.min(18), Validators.max(120)]],
      dependents: [null, [Validators.min(0)]],
    });
    this.authService
      .getUserProfile()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (user) => {
          this.currentUser = user;
          this.userForm.patchValue({
            firstName: user.firstName,
            lastName: user.lastName,
            address: user.address,
            phone: user.phone,
            job: user.job,
            income: user.income,
            costs: user.costs,
            age: user.age,
            dependents: user.dependents,
          });
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error('Failed to load user profile:', err);
          this.snackBar.open('Błąd ładowania profilu', 'OK', { duration: 3000 });
        },
      });
  }

  protected updateUserAccountData(): void {
    if (this.userForm.invalid || !this.currentUser) {
      return;
    }

    const updates = {
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      address: this.userForm.value.address,
      phone: this.userForm.value.phone,
      job: this.userForm.value.job,
      income: this.userForm.value.income,
      costs: this.userForm.value.costs,
      age: this.userForm.value.age,
      dependents: this.userForm.value.dependents,
    };

    this.userService
      .updateProfile(this.currentUser.id, updates)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (updatedUser) => {
          this.currentUser = updatedUser;
          this.snackBar.open('Profil zaktualizowany pomyślnie', 'OK', { duration: 3000 });
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error('Failed to update profile:', err);
          this.snackBar.open('Błąd aktualizacji profilu', 'OK', { duration: 3000 });
        },
      });
  }
}
