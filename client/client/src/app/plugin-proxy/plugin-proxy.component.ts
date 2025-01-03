import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { loadRemoteModule } from '@angular-architects/module-federation';
import { PluginOptions } from './plugin';

@Component({
  standalone: true,
  selector: 'plugin-proxy',
  template: `
        <ng-container #placeHolder></ng-container>
    `
})
export class PluginProxyComponent implements OnInit {
  @ViewChild('placeHolder', { read: ViewContainerRef, static: true })
  viewContainer!: ViewContainerRef;

  constructor() { }

  async ngOnInit() {
    this.viewContainer.clear();

    let options: PluginOptions = {
      type: 'module',
      remoteEntry: 'http://localhost:4201/remoteEntry.js',
      exposedModule: './Sidebar',
      displayName: 'Sidebar',
      componentName: 'SidebarComponent'
    };

    const Component = await loadRemoteModule(options)
      .then(m => m[options.componentName]);

    this.viewContainer.createComponent(Component);
  }
}

