use warnings;
use strict;

my $pdfFields = `pdftk $ARGV[0] dump_data_fields`;

my $fileName = $ARGV[0];
$fileName =~ s{.*\\}{}; 
$fileName =~ s{\.[^.]+$}{};

print "using iTextSharp.text.pdf;\n";
print "using System;\n";
print "using System.IO;\n";
print "using ApplicationGeneration.Models;\n";
print "\n";
print "namespace ApplicaitonGeneration.Helpers\n";
print "{\n";
print "    public static partial class RealtorDocument\n";
print "    {\n";
print "        static public byte[] $fileName(string inputDir, RealtorClient applicant)\n";
print "        {\n";
print "\n";
print "            var inputFile = inputDir + \"$fileName\.pdf\";\n";
print "            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);\n";
print "            var stream = new MemoryStream();\n";
print "            var stamper = new PdfStamper(reader, stream);\n";
print "\n";
print "            var fields = stamper.AcroFields;\n";
my $count = 0;
my @lines = split(/\n/, $pdfFields);
foreach my $row (@lines) {
	if ($row =~ /FieldName:/){
		$count = $count+ 1;
	    $row =~ s/^\S+\s*//;
		print "            fields.SetField(\"$row\", \"\#$count\");\n";
	}
}
print "\n";
print "            stamper.FormFlattening = true;\n";
print "            stamper.Dispose();\n";
print "\n";
print "            var result = stream.ToArray();\n";
print "\n";
print "            reader.Close();\n";
print "\n";
print "            return result;\n";
print "        }\n";
print "    }\n";
print "}\n";
