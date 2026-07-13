import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ContentCard } from '../../../catalog/components/content-card/content-card';
import { ContentService } from '../../../catalog/services/content.service';
import { Content } from '../../../catalog/models/content';

@Component({
  selector: 'app-home-page',
  imports: [ContentCard, RouterLink],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage implements OnInit {
  private readonly contentService = inject(ContentService);

  readonly contents = signal<Content[]>([]);
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.contentService.getAll().subscribe({
      next: contents => {
        this.contents.set(contents);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('No se pudo cargar el contenido.');
        this.loading.set(false);
      }
    });
  }
}