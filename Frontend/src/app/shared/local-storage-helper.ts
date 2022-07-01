export type EventIdWithEditToken = { eventId: string; editToken: string };
export type EventAndSignupIdWithEditToken = {
  eventId: string;
  signupId: string;
  editToken: string;
};

export class LocalStorageHelper {
  private static myEventsKey = 'myEvents';
  private static mySignupsKey = 'mySignups';

  static get myEventIds(): Array<EventIdWithEditToken> {
    const eventIds = localStorage.getItem(LocalStorageHelper.myEventsKey);
    if (eventIds === null) {
      return new Array<EventIdWithEditToken>();
    }

    const parsed = JSON.parse(eventIds);
    if (!Array.isArray(parsed)) {
      return new Array<EventIdWithEditToken>();
    }

    return parsed;
  }

  static get mySignupIds(): Array<EventAndSignupIdWithEditToken> {
    const eventIds = localStorage.getItem(LocalStorageHelper.mySignupsKey);
    if (eventIds === null) {
      return new Array<EventAndSignupIdWithEditToken>();
    }

    const parsed = JSON.parse(eventIds);
    if (!Array.isArray(parsed)) {
      return new Array<EventAndSignupIdWithEditToken>();
    }

    return parsed;
  }

  static addMyEvent(eventId: string, editToken: string): void {
    let myEvents = LocalStorageHelper.myEventIds;
    myEvents = [...myEvents, { eventId, editToken }];
    localStorage.setItem(
      LocalStorageHelper.myEventsKey,
      JSON.stringify(myEvents)
    );
  }

  static addMySignup(
    eventId: string,
    signupId: string,
    editToken: string
  ): void {
    let mySignups = LocalStorageHelper.mySignupIds;
    mySignups = [...mySignups, { eventId, signupId, editToken }];
    localStorage.setItem(
      LocalStorageHelper.mySignupsKey,
      JSON.stringify(mySignups)
    );
  }
}
