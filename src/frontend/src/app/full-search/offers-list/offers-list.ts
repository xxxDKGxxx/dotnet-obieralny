import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Offer } from './offer/offer';
import { CalculatedOfferDto, OfferDto } from '../../services/offers/offer-model';
import { CalculatedOffer } from './calculated-offer/calculated-offer';

@Component({
  selector: 'app-offers-list',
  imports: [Offer, CalculatedOffer],
  templateUrl: './offers-list.html',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class OffersList {
  @Input({ required: true })
  offers!: OfferDto[];
  @Input()
  calculatedOffers!: CalculatedOfferDto[];
}
