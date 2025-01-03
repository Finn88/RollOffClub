import { Component, OnInit, } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PluginProxyComponent } from './plugin-proxy/plugin-proxy.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, PluginProxyComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

}

