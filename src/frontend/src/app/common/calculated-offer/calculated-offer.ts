import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { ApplicationProviderType, CalculatedOfferDto } from '../../services/offers/offer-model';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { ProviderTypePipe } from '../provider-type-pipe';

@Component({
  selector: 'app-calculated-offer',
  imports: [MatCardModule, MatButtonModule, ProviderTypePipe],
  templateUrl: './calculated-offer.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CalculatedOffer {
  @Input({ required: true })
  calculatedOffer!: CalculatedOfferDto;
  @Input()
  detailsButton: boolean = true;

  @Output()
  detailsClicked = new EventEmitter<{ id: number; providerType: ApplicationProviderType }>();

  protected emitClickEvent() {
    this.detailsClicked.emit({
      id: this.calculatedOffer.id,
      providerType: this.calculatedOffer.providerType,
    });
  }
}
