import { Component, input } from '@angular/core';
import { Content } from '../../models/content';

@Component({
  selector: 'app-content-card',
  imports: [],
  templateUrl: './content-card.html',
  styleUrl: './content-card.css',
})
export class ContentCard {
  content = input.required<Content>();
}
