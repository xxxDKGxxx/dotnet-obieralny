import { DestroyRef, inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { apiEndpoints } from '../../api-endpoints';
import { ApplicationProviderType } from '../../shared/enum';
import { saveAs } from 'file-saver';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  private readonly http = inject(HttpClient);
  private readonly destroyRef = inject(DestroyRef);

  downloadDocumentById(
    documentId: string,
    applicationId: number,
    providerType: ApplicationProviderType,
  ) {
    const params = new HttpParams()
      .set('applicationId', applicationId)
      .set('providerType', providerType);

    this.http
      .get(apiEndpoints.documentById(documentId), {
        responseType: 'blob',
        observe: 'response',
        params: params,
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: this.handleFile,
      });
  }

  downloadDocumentTemplate(providerType: ApplicationProviderType) {
    const params = new HttpParams().set('providerType', providerType);

    this.http
      .get(apiEndpoints.documentTemplate(), {
        responseType: 'blob',
        observe: 'response',
        params: params,
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: this.handleFile,
      });
  }

  uploadDocument(applicationId: number, documentId: string, providerType: string, document: File) {
    const formData = new FormData();

    formData.append('ApplicationId', applicationId.toString());
    formData.append('DocumentId', documentId);
    formData.append('ProviderType', providerType);
    formData.append('Document', document);

    return this.http.post(apiEndpoints.documents(), formData);
  }

  private handleFile(response: HttpResponse<Blob>) {
    const contentDisposition = response.headers.get('content-disposition');
    let filename = 'plik.docx';

    if (contentDisposition) {
      const utf8Match = /filename\*=UTF-8''([^;]*)/i.exec(contentDisposition);

      if (utf8Match && utf8Match[1]) {
        filename = decodeURIComponent(utf8Match[1]);
      } else {
        const standardMatch = /filename="?([^"]+)"?/i.exec(contentDisposition);

        if (standardMatch && standardMatch[1]) {
          filename = standardMatch[1];
        }
      }
    }

    const blob = response.body as Blob;
    saveAs(blob, filename);
  }
}
