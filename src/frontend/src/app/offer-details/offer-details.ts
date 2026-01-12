import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-offer-details',
  imports: [],
  templateUrl: './offer-details.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetails implements OnInit {
  protected offerId!: number;
  protected amount!: number | null;
  protected duration!: number | null;
  protected monthlyIncome!: number | null;
  protected monthlyCosts!: number | null;
  protected age!: number | null;
  protected dependants!: number | null;
  protected providerType!: string | null;

  private readonly activatedRoute = inject(ActivatedRoute);

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params) => {
      const offerId = params.get('offerId');

      if (!offerId) {
        return;
      }

      this.offerId = Number.parseInt(offerId);

      this.activatedRoute.queryParamMap.subscribe((params) => {
        this.amount = Number.parseInt(params.get('amount') ?? '');
        this.duration = Number.parseInt(params.get('duration') ?? '');
        this.monthlyIncome = Number.parseInt(params.get('monthlyIncome') ?? '');
        this.monthlyCosts = Number.parseInt(params.get('monthlyCosts') ?? '');
        this.age = Number.parseInt(params.get('age') ?? '');
        this.dependants = Number.parseInt(params.get('dependants') ?? '');
        this.providerType = params.get('providerType');
      });
    });
  }

  protected additionalInfoProvided(): boolean {
    return !!this.monthlyIncome && !!this.monthlyCosts && !!this.age && !!this.dependants;
  }

  protected fetchOffer() {
    if (!!this.amount || !this.duration) {
      return;
    }
  }
}
