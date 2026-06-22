import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Article } from '../article.model';

interface FreshnessCount {
  label: string;
  count: number;
}

@Component({
  selector: 'app-freshness-summary',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="summary">
      @for (band of counts; track band.label) {
        <span class="chip">{{ band.label }}: {{ band.count }}</span>
      }
    </div>
  `,
  styles: [`
    .summary {
      margin: 12px 0;
    }

    .chip {
      margin-right: 10px;
      font-size: 0.85rem;
      padding: 2px 10px;
      border-radius: 12px;
      background: #f0f0f5;
    }
  `],
})
export class FreshnessSummaryComponent {
  @Input({ required: true }) articles: Article[] = [];

  get counts(): FreshnessCount[] {
    const buckets = { New: 0, Recent: 0, Stale: 0 };

    for (const article of this.articles) {
      if (!article.publishedAt) {
        continue;
      }

      const days = (Date.now() - new Date(article.publishedAt).getTime()) / 86400000;

      if (days < 7) {
        buckets.New += 1;
      } else if (days < 30) {
        buckets.Recent += 1;
      } else {
        buckets.Stale += 1;
      }
    }

    return [
      { label: 'New', count: buckets.New },
      { label: 'Recent', count: buckets.Recent },
      { label: 'Stale', count: buckets.Stale },
    ];
  }
}
