import { Component, OnInit, ChangeDetectionStrategy, ViewEncapsulation } from '@angular/core';
import { SiteTheme } from 'src/app/models/customtheme';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-theme-picker',
  templateUrl: './theme-picker.component.html',
  styleUrls: ['./theme-picker.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class ThemePickerComponent implements OnInit {

  currentTheme;

  themes: SiteTheme[] = [
    {
      primary: '#673AB7',
      accent: '#FFC107',
      displayName: 'Morado Oscuro y Ámbar',
      name: 'deeppurple-amber',
    },
    {
      primary: '#3F51B5',
      accent: '#E91E63',
      displayName: 'Indigo & Rosa',
      name: 'indigo-pink',
    },
    {
      primary: '#E91E63',
      accent: '#607D8B',
      displayName: 'Rosa & Gris-Azulado',
      name: 'pink-bluegrey',
    },
    {
      primary: '#9C27B0',
      accent: '#4CAF50',
      displayName: 'Morado & Verde',
      name: 'purple-green',
    },
  ];

  constructor(public themeService: ThemeService) { }

  ngOnInit() {
    this.installTheme('indigo-pink');
  }

  installTheme(themeName: string) {
    this.currentTheme = this.themes.find(theme => theme.name === themeName);
    if (!this.currentTheme) {
      return;
    }

    this.themeService.setStyle('theme', `${this.currentTheme.name}.css`);
  }
}
