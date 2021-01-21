import { Component, OnInit } from '@angular/core';
import { NbDialogService } from '@nebular/theme';
import { Message } from 'src/app/models/message';
import { Receiver } from 'src/app/models/receiver';
import { ChatService } from 'src/app/services/chat.service';
import { MessageService } from 'src/app/services/message.service';
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
    private messageService: MessageService,
    private dialogService: NbDialogService) { }

  ngOnInit(): void {
    this.messageService.getReceivers().subscribe((data: Receiver[]) => {      
      this.receivers = data;
    })
  }

  onSelectReceiver($event: number) {
    this.messageService.getMessages($event).subscribe(data => {
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
      if(data) {
        this.messageService.sendMessage(data).subscribe();
      }
    })



  }
}
