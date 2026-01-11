import { Pipe, type PipeTransform } from '@angular/core';
import { ApplicationProviderType } from '../../../services/offers/offer-model';

@Pipe({
  name: 'appProviderType',
})
export class ProviderTypePipe implements PipeTransform {
  private readonly map: Record<ApplicationProviderType, string> = {
    [ApplicationProviderType.ArdalisBank]: 'ArdalisBank',
  };
  transform(value: ApplicationProviderType | string): string {
    if (typeof value === 'string' && value in ApplicationProviderType) {
      const mapped = ApplicationProviderType[value as keyof typeof ApplicationProviderType];
      if (typeof mapped === 'number') {
        return this.map[mapped];
      }
    }
    return this.map[value as ApplicationProviderType];
  }
}
