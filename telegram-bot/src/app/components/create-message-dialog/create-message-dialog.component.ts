import { Component, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-create-message-dialog',
  templateUrl: './create-message-dialog.component.html',
  styleUrls: ['./create-message-dialog.component.scss']
})
export class CreateMessageDialogComponent implements OnInit {

  message: string = '';
  chatId: number;
  buttons: string[] = []
  constructor(private ref: NbDialogRef<CreateMessageDialogComponent>) { }

  ngOnInit(): void {
  }

  addButton(button: string) {
    this.buttons.push(button);
  }

  sendMessage() {
    const message = new Message();
    message.buttons = this.buttons;
    message.chatId = +this.chatId;
    message.messageText = this.message;

    this.ref.close(message)
  }
}
