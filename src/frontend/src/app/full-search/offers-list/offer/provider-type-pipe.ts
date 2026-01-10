import { Pipe, type PipeTransform } from '@angular/core';
import { ApplicationProviderType } from '../../../services/offers/offer-model';

@Pipe({
  name: 'appProviderType',
})
export class ProviderTypePipe implements PipeTransform {
  private readonly map: Record<ApplicationProviderType, string> = {
    [ApplicationProviderType.ArdalisBank]: 'ArdalisBank',
  };
  transform(value: ApplicationProviderType): string {
    return this.map[value];
  }
}
