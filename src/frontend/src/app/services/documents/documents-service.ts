import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { apiEndpoints } from '../../api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  private readonly http = inject(HttpClient);

  uploadDocument(applicationId: number, documentId: string, providerType: string, document: File) {
    const formData = new FormData();

    formData.append('ApplicationId', applicationId.toString());
    formData.append('DocumentId', documentId);
    formData.append('ProviderType', providerType);
    formData.append('Document', document);

    return this.http.post(apiEndpoints.documents(), formData);
  }
}
