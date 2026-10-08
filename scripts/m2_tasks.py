import subprocess
r = subprocess.run(["tasklist", "/FI", "IMAGENAME eq idat.exe", "/FO", "CSV", "/V"],
                   capture_output=True, text=True, timeout=40)
print(r.stdout[:3000])
r2 = subprocess.run(["tasklist", "/FI", "IMAGENAME eq ida.exe", "/FO", "CSV", "/V"],
                    capture_output=True, text=True, timeout=40)
print("--- ida.exe ---")
print(r2.stdout[:3000])