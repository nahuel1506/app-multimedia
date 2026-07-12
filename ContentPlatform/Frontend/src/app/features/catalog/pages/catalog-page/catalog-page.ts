import { Component, inject, OnInit } from '@angular/core';
import { Content } from '../../models/content';
import { ContentService } from '../../services/content.service';

@Component({
  selector: 'app-catalog-page',
  imports: [],
  templateUrl: './catalog-page.html',
  styleUrl: './catalog-page.css'
})
export class CatalogPage implements OnInit {
  private readonly contentService = inject(ContentService);

  contents: Content[] = [];

  ngOnInit(): void {
    this.contentService.getAll().subscribe({
      next: contents => {
        this.contents = contents;
      },
      error: error => {
        console.error('Error cargando el catálogo', error);
      }
    });
  }
}