# Creates a PFX signing certificate and BASE64 encodes it to a text file
# Needs admin priviledges
$cert = New-SelfSignedCertificate -Type CodeSigningCert -Subject "CN=MyApp Developer" -CertStoreLocation Cert:\CurrentUser\My
Export-PfxCertificate -Cert $cert -FilePath "C:\mycert.pfx" -Password (ConvertTo-SecureString "mypassword" -AsPlainText -Force)
$fileContentBytes = get-content 'C:\mycert.pfx' -Encoding Byte
[System.Convert]::ToBase64String($fileContentBytes) | Out-File 'D:\pfx-bytes.txt'