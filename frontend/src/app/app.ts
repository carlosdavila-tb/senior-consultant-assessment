import { Component } from '@angular/core';
import { ArticlesPageComponent } from './features/articles/containers/articles-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ArticlesPageComponent],
  template: `<app-articles-page></app-articles-page>`,
})
export class App {}
