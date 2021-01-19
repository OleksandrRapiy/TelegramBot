import { Component, OnInit } from '@angular/core';
import { NbDialogService } from '@nebular/theme';
import { Message } from 'src/app/models/message';
import { Receiver } from 'src/app/models/receiver';
import { ChatService } from 'src/app/services/chat.service';
import { CreateMessageDialogComponent } from '../create-message-dialog/create-message-dialog.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  receivers: Receiver[] = [];

  messages: any[] = [];
  chatId: number;

  constructor(private chatService: ChatService,
    private dialogService: NbDialogService) { }

  ngOnInit(): void {
    this.chatService.getReceivers().subscribe((data: Receiver[]) => {
      this.receivers = data;
    })
  }

  onSelectReceiver($event: number) {
    this.chatService.getMessages($event).subscribe(data => {
      this.messages = data;
      this.chatId = $event
    });
  }

  sendMessage($event) {

    this.dialogService.open(CreateMessageDialogComponent, {
      context: {
        message: $event.message,
        chatId: this.chatId
      },
      dialogClass: 'create-message-dialog'
    }).onClose.subscribe((data: Message) => {
      this.chatService.sendMessage(data).subscribe(() => {

      })
    })



  }
}
