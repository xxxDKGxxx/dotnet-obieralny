import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { OfferDto } from '../../../services/offers/offer-model';

@Component({
  selector: 'app-offer',
  imports: [MatCardModule],
  templateUrl: './offer.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Offer {
  @Input({ required: true })
  offer!: OfferDto;
}
