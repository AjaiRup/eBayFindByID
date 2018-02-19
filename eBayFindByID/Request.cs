using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using eBayFindByID.FindingAPI;

namespace eBayFindByID
{
    public class Request
    {
        public static void New()
        {
            using (FindingServicePortTypeClient client = new FindingServicePortTypeClient())
            {
                MessageHeader header = MessageHeader.CreateHeader("CustomHeader", "", "");
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", "AjaiRupa-Test-PRD-a43252cae-252f99e0");
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByProduct");
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                    FindItemsByProductRequest request = new FindItemsByProductRequest();
                    var prod = new ProductId();
                    request.productId = prod;
                    prod.type = "UPC";
                    prod.Value = Number.UpcNumber;
                    FindItemsByProductResponse response = client.findItemsByProduct(request);
                    if (response.ack != AckValue.Success)
                    {
                        Console.WriteLine(response.errorMessage[0].message + "\nPress escape to exit");
                        if (Console.ReadKey().Key.ToString() != "Escape")
                        {
                            UserInput.Start();
                            Request.New();
                        }
                    }
                    else if (response.searchResult.item == null)
                    {
                        Console.WriteLine("No eBay item available for UPC Number " + Number.UpcNumber +
                                          "\nPress enter to try another UPC");
                        if (Console.ReadKey().Key.ToString() == "Enter")
                        {
                            UserInput.Start();
                            Request.New();
                        }
                    }
                    else
                    {
                        foreach (var item in response.searchResult.item)
                        {
                            Console.WriteLine("Item Title: " + item.title);
                            Console.WriteLine("Item Price: $" + item.sellingStatus.convertedCurrentPrice.Value + "\n");
                        }
                        Console.WriteLine("Press enter to perform another search\n");
                        if (Console.ReadKey().Key.ToString() == "Enter")
                        {
                            UserInput.Start();
                            Request.New();
                        }
                    }


                }

            }
        }
    }
}
