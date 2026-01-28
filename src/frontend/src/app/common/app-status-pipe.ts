import { Pipe, PipeTransform } from '@angular/core';
import { ApplicationStatus } from '../services/applications/applications-model';

@Pipe({
  name: 'appStatus',
})
export class AppStatusPipe implements PipeTransform {
  private readonly applicationStatusMap: Record<ApplicationStatus, string> = {
    [ApplicationStatus.Created]: 'Utworzona',
    [ApplicationStatus.AwaitingSignature]: 'Czeka na podpis',
    [ApplicationStatus.Signed]: 'Podpisana',
    [ApplicationStatus.Granted]: 'Zaakceptowana',
    [ApplicationStatus.AwaitingAmendments]: 'Czeka na poprawki',
    [ApplicationStatus.Rejected]: 'Odrzucona',
    [ApplicationStatus.Withdrawn]: 'Wycofana',
  };
  transform(value: ApplicationStatus): string {
    return this.applicationStatusMap[value as ApplicationStatus];
  }
}
