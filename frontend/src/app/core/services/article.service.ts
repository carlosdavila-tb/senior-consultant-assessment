import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Article } from '../../features/articles/article.model';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/articles`;

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.baseUrl);
  }
}
