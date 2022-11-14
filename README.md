# SpirePdf.Degree.Example

The main objective of this project is to share a code example so that an alignment problem when generating a PDF with the SpirePDF library can be simulated.

When running the `SpirePdf.Degree.Console` project, a Degree.pdf file is generated, when opening this file it is possible to observe that the text alignment is correctly centered (except for the title, but this is not the problem in question).

When running the SpirePdf.Degree.Api project and performing a GET on the `https://localhost:7280/degree` endpoint the first or second time the endpoint is triggered the PDF file is generated correctly, but if you keep trying on the second or third time the file will be generated with an incorrect alignment.

Both `SpirePdf.Degree.Console` and `SpirePdf.Degree.Api` projects use the `SpirePdf.PdfGenerator` project to generate the PDF file, and there is no difference in the code that generates the PDF file, only in the way it is called, one of them by a Console Application and the other by a Rest API.