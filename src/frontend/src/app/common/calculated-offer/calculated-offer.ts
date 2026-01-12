import { ChangeDetectionStrategy, Component, input, Input } from '@angular/core';
import { CalculatedOfferDto } from '../../services/offers/offer-model';
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
}
