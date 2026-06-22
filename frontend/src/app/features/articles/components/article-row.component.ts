import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Article } from '../article.model';

@Component({
  selector: 'app-article-row',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="row">
      <div class="main">
        <span class="title">{{ article.title }}</span>
        <span class="author">{{ article.authorName }}</span>
      </div>
      <span class="badge">{{ freshness }}</span>
    </div>
  `,
  styles: [`
    .row {
      padding: 10px 8px;
      border-bottom: 1px solid #ddd;
    }

    .main {
      margin-bottom: 4px;
    }

    .title {
      font-weight: 600;
      margin-right: 8px;
    }

    .author {
      color: #666;
      font-size: 0.85rem;
    }

    .badge {
      font-size: 0.8rem;
      padding: 2px 8px;
      border-radius: 12px;
      background: #eef;
    }
  `],
})
export class ArticleRowComponent {
  @Input({ required: true }) article!: Article;

  get freshness(): string {
    if (!this.article.publishedAt) {
      return 'Unpublished';
    }

    const days = (Date.now() - new Date(this.article.publishedAt).getTime()) / 86400000;

    if (days < 7) {
      return 'New';
    } else if (days < 30) {
      return 'Recent';
    } else if (days < 365) {
      return 'Stale';
    }
    
    return 'Archived';
  }
}
