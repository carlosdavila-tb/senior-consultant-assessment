import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticleService } from '../../../core/services/article.service';
import { Article } from '../article.model';
import { ArticleRowComponent } from '../components/article-row.component';
import { FreshnessSummaryComponent } from '../components/freshness-summary.component';

@Component({
  selector: 'app-articles-page',
  standalone: true,
  imports: [CommonModule, ArticleRowComponent, FreshnessSummaryComponent],
  template: `
    <section class="page">
      <h1>Editorial — Articles</h1>

      <app-freshness-summary [articles]="articles"></app-freshness-summary>

      <div class="list">
        @for (article of articles; track article.id) {
          <app-article-row [article]="article"></app-article-row>
        }
      </div>
    </section>
  `,
  styles: [`
    .page {
      max-width: 760px;
      margin: 0 auto;
      padding: 24px;
      font-family: system-ui, sans-serif;
    }

    h1 {
      font-size: 1.4rem;
    }
  `],
})
export class ArticlesPageComponent implements OnInit {
  articles: Article[] = [];

  private articleService = inject(ArticleService);

  ngOnInit(): void {
    this.articleService.getArticles().subscribe({
      next: (articles) => (this.articles = articles),
      error: (err) => console.error('Failed to load articles', err),
    });
  }
}
