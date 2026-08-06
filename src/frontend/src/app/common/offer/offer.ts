import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { OfferDto } from '../../services/offers/offer-model';
import { ProviderTypePipe } from '../provider-type-pipe';
import { MatButtonModule } from '@angular/material/button';
import { ApplicationProviderType } from '../../shared/enum';

@Component({
  selector: 'app-offer',
  imports: [MatCardModule, ProviderTypePipe, MatButtonModule],
  templateUrl: './offer.html',
})
export class Offer {
  @Input({ required: true })
  offer!: OfferDto;
  @Input()
  detailsButton: boolean = true;

  @Output()
  detailsClicked = new EventEmitter<{ id: number; providerType: ApplicationProviderType }>();

  protected emitClickEvent() {
    this.detailsClicked.emit({
      id: this.offer.id,
      providerType: this.offer.providerType,
    });
  }
}
