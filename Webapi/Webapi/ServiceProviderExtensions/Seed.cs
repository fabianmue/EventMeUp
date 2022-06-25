using Webapi.Models.Comments;
using Webapi.Models.Events;
using Webapi.Models.Signups;
using Webapi.Repositories.Events;
using Webapi.Repositories.Signups;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void Seed(this IServiceProvider serviceProvider)
  {
    var eventRepository = serviceProvider.GetRequiredService<IEventRepository>();
    var signupRepository = serviceProvider.GetRequiredService<ISignupRepository>();

    List<Event> createdEvents = SeedEvents(eventRepository, EventCreateDtos);
    List<Signup> createdSignups = SeedSignups(eventRepository, SignupCreateDtos, createdEvents);
    List<Comment> createdComments = SeedComments(signupRepository, CommentCreateDtos, createdSignups);
  }

  private static List<Event> SeedEvents(
    IEventRepository eventRepository,
    List<EventCreateDto> eventCreateDtos)
  {
    List<Event> events = eventCreateDtos
      .Select(eventCreateDto => new Event(eventCreateDto))
      .ToList();
    eventRepository.AddRange(events);
    eventRepository.SaveChanges();
    return events;
  }

  private static List<Signup> SeedSignups(
    IEventRepository eventRepository,
    List<SignupCreateDto> signupCreateDtos,
    List<Event> createdEvents
  )
  {
    List<Signup> signups = signupCreateDtos
      .Select(signUpCreateDto => new Signup(signUpCreateDto))
      .ToList();
    createdEvents.ForEach(ev =>
    {
      ev.Signups.AddRange(signups);
      eventRepository.Update(ev);
    });
    eventRepository.SaveChanges();
    return signups;
  }

  private static List<Comment> SeedComments(
    ISignupRepository signupRepository,
    List<CommentCreateDto> commentCreateDtos,
    List<Signup> createdSignups)
  {
    List<Comment> comments = commentCreateDtos
      .Select(commentCreateDto => new Comment(commentCreateDto))
      .ToList();
    var random = new Random();
    createdSignups.ForEach(signup =>
    {
      signup.Comments.Add(comments[random.Next(comments.Count)]);
      signupRepository.Update(signup);
    });
    signupRepository.SaveChanges();
    return comments;
  }

  private static readonly List<EventCreateDto> EventCreateDtos = new()
  {
    new EventCreateDto
    {
      Title = "Squash - blood sweat and tears (of joy!)",
      Description = "It's all fun and games until...",
      Start = new DateTime(2022, 5, 24, 12, 0, 0).ToUniversalTime(),
      Category = EventCategory.Sports,
      Location = "Airgate, Oerlikon"
    },
    new EventCreateDto
    {
      Title = "Squash - the sweet squashvenge",
      Description = "Fool me once, shame on me. Fool me twice, shame on - wait, what?",
      Start = new DateTime(2022, 5, 31, 18, 30, 0).ToUniversalTime(),
      End = new DateTime(2022, 5, 31, 19, 15, 0).ToUniversalTime(),
      Location = "Vitis, Schlieren"
    },
    new EventCreateDto
    {
      Title = "Signature gathering",
      Description = "Everything for the dachshund, everything for the club.",
      Start = new DateTime(2022, 08, 06, 11, 30, 0).ToUniversalTime(),
      End = new DateTime(2022, 08, 06, 13, 00, 0).ToUniversalTime(),
      Category = EventCategory.Political,
      Location = "Bahnhofsstrasse, Zürich"
    },
    new EventCreateDto
    {
      Title = "THE Party",
      Description = "Not just any party - THE party!",
      Start = new DateTime(2022, 08, 06, 19, 30, 0).ToUniversalTime(),
      End = new DateTime(2022, 08, 07, 4, 00, 0).ToUniversalTime(),
      Category = EventCategory.Social,
      Location = "WG SH South, Binz"
    },
    new EventCreateDto
    {
      Title = "New years eve",
      Description = "This gon' be gud",
      Start = new DateTime(2022, 12, 31, 22, 0, 0).ToUniversalTime(),
      End = new DateTime(2023, 01, 01, 2, 00, 0).ToUniversalTime(),
      Category = EventCategory.Social,
      Location = "WG SH South, Binz"
    }
  };

  private static readonly List<SignupCreateDto> SignupCreateDtos = new()
  {
    new SignupCreateDto
    {
      CreatedBy = "Monica",
      Status = SignupStatus.Accepted
    },
    new SignupCreateDto
    {
      CreatedBy = "Erica",
      Status = SignupStatus.Tentative,
    },
    new SignupCreateDto
    {
      CreatedBy = "Rita",
      Status = SignupStatus.Declined,
    },
    new SignupCreateDto
    {
      CreatedBy = "Tina",
      Status = SignupStatus.Declined,
    },
    new SignupCreateDto
    {
      CreatedBy = "Sandra",
      Status = SignupStatus.Accepted,
    },
    new SignupCreateDto
    {
      CreatedBy = "Mary",
      Status = SignupStatus.Accepted,
    },
    new SignupCreateDto
    {
      CreatedBy = "Jessica",
      Status = SignupStatus.Tentative,
    }
  };

  private static readonly List<CommentCreateDto> CommentCreateDtos = new()
  {
    new CommentCreateDto
    {
      Text = "Can't make it before 14:00"
    },
    new CommentCreateDto
    {
      Text = "Propose to reschedule the week after"
    },
    new CommentCreateDto
    {
      Text = "Great event, love it!"
    }
  };
}