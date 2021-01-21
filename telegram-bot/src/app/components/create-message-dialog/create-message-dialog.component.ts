import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NbDialogRef } from '@nebular/theme';
import { Button } from 'src/app/models/button';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-create-message-dialog',
  templateUrl: './create-message-dialog.component.html',
  styleUrls: ['./create-message-dialog.component.scss']
})
export class CreateMessageDialogComponent implements OnInit {
  
  message: string = '';
  chatId: number;
  buttons: Button[] = [];
  
  constructor(private ref: NbDialogRef<CreateMessageDialogComponent>) { }

  ngOnInit(): void {
  }

  addButton(buttonName: string, buttonAction: string) {
    this.buttons.push({
      name: buttonName,
      action: buttonAction
    });
  }


  sendMessage(messageText: string) {
    const message = new Message();
    message.buttons = this.buttons;
    message.chatId = +this.chatId;
    message.messageText = messageText;

    this.ref.close(message)
  }
}
