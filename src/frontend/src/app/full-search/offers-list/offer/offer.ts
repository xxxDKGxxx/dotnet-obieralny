import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { OfferDto } from '../../../services/offers/offer-model';
import { ProviderTypePipe } from './provider-type-pipe';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-offer',
  imports: [MatCardModule, ProviderTypePipe, MatButtonModule],
  templateUrl: './offer.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Offer {
  @Input({ required: true })
  offer!: OfferDto;
}
