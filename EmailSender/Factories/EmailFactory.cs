using EmailSender.Models;

namespace EmailSender.Factories;

public class EmailFactory
{
    public static EmailModel Create(InputModel model)
    {
        if (model != null && model.Email != null)
        {
            if (model.Category != null && model.Name != null && model.Phone != null)
            {
                return new EmailModel
                {
                    To = model.Email,
                    Subject = $"Onatrix - Thank you for contacting us {model.Name}!",
                    HtmlContent = $"<html><body><h1 style='text-align: center;'>Onatrix</h1><p><strong>We will get back to you shortly about {model.Category.ToLower()}!</strong></p><p>We will email you, or call you on phone number {model.Phone}.</p><p>Your case number is: {model.Id}</p></body></html>",
                    PlainTextContent = $"Onatrix\r\nWe will get back to you shortly!\r\nYour case number is: {model.Id}"
                };
            }
            else if (model.Name != null && model.Question != null)
            {
                return new EmailModel
                {
                    To = model.Email,
                    Subject = $"Onatrix - Thank you for contacting us {model.Name}!",
                    HtmlContent = $"<html><body><h1 style='text-align: center;'>Onatrix</h1><p><strong>We will get back to you shortly concerning you question:</strong></p><p>{model.Question}</p><p>Your case number is: {model.Id}</p></body></html>",
                    PlainTextContent = $"Onatrix\r\nWe will get back to you shortly!\r\nYour case number is: {model.Id}"
                };
            }
             
            return new EmailModel
            {
                To = model.Email,
                Subject = "Onatrix - Thank you for contacting us!",
                HtmlContent = $"<html><body><h1 style='text-align: center;'>Onatrix</h1><p><strong>We will get back to you shortly!</strong></p><p>Your case number is: {model.Id}</p></body></html>",
                PlainTextContent = $"Onatrix\r\nWe will get back to you shortly!\r\nYour case number is: {model.Id}"
            };
            
        }

        return null!;
    }
}
