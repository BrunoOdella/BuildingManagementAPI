import { Component, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-toggle-theme',
  template: `
    <div class="toggle-theme">
      <label class="switch">
        <input type="checkbox" (change)="toggleTheme($event)">
        <span class="slider round"></span>
      </label>
      <span class="icon">{{ isDarkMode ? '🌙' : '☀️' }}</span>
    </div>
  `,
  styleUrls: ['./toggle-theme.component.css']
})
export class ToggleThemeComponent {
  isDarkMode = false;

  constructor(private renderer: Renderer2) {}

  toggleTheme(event: any): void {
    this.isDarkMode = event.target.checked;
    if (this.isDarkMode) {
      this.renderer.addClass(document.body, 'dark-mode');
    } else {
      this.renderer.removeClass(document.body, 'dark-mode');
    }
  }
}
