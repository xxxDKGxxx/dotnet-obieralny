import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { Offer } from '../../common/offer/offer';
import { CalculatedOfferDto, OfferDto } from '../../services/offers/offer-model';
import { CalculatedOffer } from '../../common/calculated-offer/calculated-offer';
import { ApplicationProviderType } from '../../shared/enum';

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

  @Output()
  detailsClicked = new EventEmitter<{ id: number; providerType: ApplicationProviderType }>();

  emitDetailsClicked(data: { id: number; providerType: ApplicationProviderType }) {
    this.detailsClicked.emit(data);
  }
}
