import { Component, DestroyRef, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-document-upload',
  imports: [MatCardModule, MatInputModule, MatFormFieldModule, MatIconModule, MatButtonModule],
  templateUrl: './document-upload.html',
})
export class DocumentUpload implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  protected applicationId!: number;

  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  private documentId!: string;

  selectedFile: File | null = null;
  isDragging = false;
  errorMessage: string | null = null;

  protected onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.handleFile(input.files[0]);
    }
  }

  protected onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    if (event.dataTransfer?.files?.length) {
      this.handleFile(event.dataTransfer.files[0]);
    }
  }

  private handleFile(file: File): void {
    this.errorMessage = null;
    const isDocx = file.name.toLowerCase().endsWith('.docx');

    if (!isDocx) {
      this.errorMessage = 'Nieprawidłowy format pliku. Wybierz dokument z rozszerzeniem .docx';
      this.selectedFile = null;
      return;
    }

    if (file.size > 10 * 1024 * 1024) {
      this.errorMessage = 'Plik jest zbyt duży (max. 10MB)';
      this.selectedFile = null;
      return;
    }

    this.selectedFile = file;
  }

  onDragOver(e: DragEvent) {
    e.preventDefault();
    this.isDragging = true;
  }
  onDragLeave(e: DragEvent) {
    e.preventDefault();
    this.isDragging = false;
  }

  removeFile(): void {
    this.selectedFile = null;
    this.errorMessage = null;
    if (this.fileInput) this.fileInput.nativeElement.value = '';
  }

  uploadDocument(): void {}

  ngOnInit(): void {
    this.activatedRoute.queryParams.pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (params) => {
        const applicationId = params['applicationId'];
        const documentId = params['documentId'];

        if (!applicationId || !documentId) {
          this.router.navigateByUrl('/');
          return;
        }

        const applicationIdAsNumber = Number.parseInt(applicationId);

        if (!applicationIdAsNumber) {
          this.router.navigateByUrl('/');
          return;
        }
        this.applicationId = applicationIdAsNumber;
        this.documentId = documentId;
      },
    });
  }
}
