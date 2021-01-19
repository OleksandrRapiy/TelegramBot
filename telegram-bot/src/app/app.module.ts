import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbSelectComponent, NbSelectModule, NbCardModule, NbChatModule, NbDialogModule, NbButtonModule, NbInputModule, NbActionsModule, NbFormFieldModule, NbIconModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { ChatComponent } from './components/chat/chat.component';
import { CreateMessageDialogComponent } from './components/create-message-dialog/create-message-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    CreateMessageDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'cosmic' }),
    NbDialogModule.forChild(),
    NbDialogModule,
    NbLayoutModule,
    NbEvaIconsModule,
    NbCardModule,
    NbSelectModule,
    NbChatModule,
    NbInputModule,
    NbActionsModule,
    NbButtonModule,
    NbFormFieldModule,
    NbIconModule
  ],
  providers: [],
  entryComponents: [
    CreateMessageDialogComponent,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
